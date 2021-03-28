using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.Editor.Common;
using Sytko.Blazor.Editor.Models;

namespace Sytko.Blazor.Editor
{
    public partial class EditorView
    {
        [Inject]
        public ILogger<EditorView> _logger { get; set; }

        [Parameter]
        public List<DragItem> Items { get; set; } = new();

        [Parameter]
        public RenderFragment<DragItem> DragItemTemplate { get; set; }

        [Parameter]
        public DragItem SelectedItem { get; set; }

        [Parameter]
        public EventCallback<DragItem> SelectedItemChanged { get; set; }

        [Parameter]
        public EventCallback<EditorActionTypes> CurrentActionChanged { get; set; }

        
        private EditorActionTypes _currentAction;

        private Vector2Int _dragItemStartOffset = Vector2Int.Zero;
        private Vector2Int _dragWorldStartOffset = Vector2Int.Zero;
        private StartItemActionHolder _resizeItemStart = null;



        public Vector2Int LocalMousePointer = Vector2Int.Zero;

        public float Zoom { get; set; } = 1.0f;


        public Vector2Int ViewportPosition { get; set; } = new Vector2Int(500, 300);


        protected override void OnInitialized()
        {
            _logger.LogInformation($"Init EditorView");
        }

        public Vector2Int ConvertPositionFromWorldToMatrix(Vector2Int point)
        {
            var posX = (point.X - ViewportPosition.X) / Zoom;
            var posY = (point.Y - ViewportPosition.Y) / Zoom;
            return new Vector2Int((int)posX, (int)posY);
        }

        public Rectangle ConvertRectangleFromMatrixToWorld(Rectangle rectangle)
        {
            var posX = Math.Floor(ViewportPosition.X + (rectangle.X * Zoom));
            var posY = Math.Floor(ViewportPosition.Y + (rectangle.Y * Zoom));
            var width = Math.Ceiling(rectangle.Width * Zoom);
            var height = Math.Ceiling(rectangle.Height * Zoom);

            return new Rectangle((int)posX, (int)posY, (int)width, (int)height);
        }

        private async void MouseMoveExecute(MouseEventArgs e)
        {
            var localMouse = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            LocalMousePointer = localMouse;

            if (_currentAction == EditorActionTypes.DragItem || _currentAction == EditorActionTypes.MoveItem)
            {
                if (SelectedItem == null)
                {
                    return;
                }

                SelectedItem.X = (int)((localMouse.X - _dragItemStartOffset.X));
                SelectedItem.Y = (int)((localMouse.Y - _dragItemStartOffset.Y));


                SetAction(EditorActionTypes.MoveItem, true);

            }
            else if (_currentAction == EditorActionTypes.DragWorld || _currentAction == EditorActionTypes.MoveWorld)
            {
                ViewportPosition = new Vector2Int((int)(e.OffsetX - _dragWorldStartOffset.X), (int)(e.OffsetY - _dragWorldStartOffset.Y));
                SetAction(EditorActionTypes.MoveWorld, true);
            }
            else if (_currentAction == EditorActionTypes.ResizeItem && _resizeItemStart != null)
            {
                //var centerItemPoint = new Vector2Int(_resizeItemStart.ItemRectangle.X + (_resizeItemStart.ItemRectangle.Width / 2), _resizeItemStart.ItemRectangle.Y + (_resizeItemStart.ItemRectangle.Height / 2));
                //var prevDistance = GetDistance(centerItemPoint.X, centerItemPoint.Y, _resizeItemStart.MatrixMousePosition.X, _resizeItemStart.MatrixMousePosition.Y);

                //var currentDistance = GetDistance(centerItemPoint.X, centerItemPoint.Y, localMouse.X, localMouse.Y);

                //var distanceRatio = (float)currentDistance / (float)prevDistance;

                //var newWidth = _resizeItemStart.ItemRectangle.Width * distanceRatio;
                //var newHeight = _resizeItemStart.ItemRectangle.Height * distanceRatio;

                //_logger.LogInformation($"PrevDistance: {prevDistance}, CurrentDistance: {currentDistance}, DistanceRatio: {distanceRatio}");

                //SelectedItem.Width = (int)newWidth;
                //SelectedItem.Height = (int)newHeight;

                //---

                var diffX = localMouse.X - _resizeItemStart.MatrixMousePosition.X;
                var diffY = localMouse.Y - _resizeItemStart.MatrixMousePosition.Y;

                SelectedItem.Width = _resizeItemStart.ItemRectangle.Width + diffX;
                var ratio = (float)SelectedItem.Width / (float)_resizeItemStart.ItemRectangle.Width;

                SelectedItem.Height = (int)(_resizeItemStart.ItemRectangle.Height * ratio);

                SelectedItem.Width = Math.Max(SelectedItem.Width, 50);
                SelectedItem.Height = Math.Max(SelectedItem.Height, 50);

                await CurrentActionChanged.InvokeAsync(_currentAction);
            }
        }

        private static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private async void MouseDownExecute(MouseEventArgs e)
        {
            if (_currentAction != EditorActionTypes.None)
            {
                return;
            }


            SelectedItem = null;
            await SelectedItemChanged.InvokeAsync(SelectedItem);

            _dragWorldStartOffset = new Vector2Int((int)((float)e.OffsetX - ViewportPosition.X), (int)((float)e.OffsetY - ViewportPosition.Y));
            SetAction(EditorActionTypes.DragWorld);
        }

        private async void MouseDownItemExecute(MouseEventArgs e, DragItem item)
        {
            if (_currentAction != EditorActionTypes.None)
            {
                return;
            }

            SelectedItem = item;
            Items.Remove(SelectedItem);
            Items.Add(SelectedItem);
            await SelectedItemChanged.InvokeAsync(SelectedItem);

            var localMouse = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            _dragItemStartOffset = new Vector2Int(localMouse.X - item.X, localMouse.Y - item.Y);
            SetAction(EditorActionTypes.DragItem);
        }

        private void MouseUpExecute(MouseEventArgs e)
        {
            SetAction(EditorActionTypes.None);
        }

        private async void MouseWheelExecute(WheelEventArgs e)
        {
            var localMouseBefore = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));

            if (e.DeltaY > 0)
            {
                Zoom -= 0.1f;
            }
            else
            {
                Zoom += 0.1f;
            }
            Zoom = Math.Max(Zoom, 0.2f);

            var localMouseAfter = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            var offset = new Vector2Int(localMouseAfter.X - localMouseBefore.X, localMouseAfter.Y - localMouseBefore.Y);
            ViewportPosition = new Vector2Int(ViewportPosition.X + offset.X, ViewportPosition.Y + offset.Y);

            await CurrentActionChanged.InvokeAsync(_currentAction);
        }

        private async void SetAction(EditorActionTypes newAction, bool forceUpdate = false)
        {
            if (_currentAction == newAction && !forceUpdate)
            {
                return;
            }

            _currentAction = newAction;
            _logger.LogInformation($"Action: {_currentAction}");
            await CurrentActionChanged.InvokeAsync(_currentAction);
        }

        public void StartResizeItem(MouseEventArgs e)
        {
            _resizeItemStart = new StartItemActionHolder
            {
                ItemRectangle = new Rectangle(SelectedItem.X, SelectedItem.Y, SelectedItem.Width, SelectedItem.Height),
                WorldMousePosition = new Vector2Int((int)e.OffsetX, (int)e.OffsetY),
                MatrixMousePosition = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY))
            };

            SetAction(EditorActionTypes.ResizeItem);
        }
    }
}
