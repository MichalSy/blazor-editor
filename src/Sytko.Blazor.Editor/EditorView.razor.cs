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
using Sytko.Blazor.Editor.Managers;

namespace Sytko.Blazor.Editor
{
    public partial class EditorView
    {
        [Inject]
        public ILogger<EditorView> _logger { get; set; }

        [Inject]
        public IEditorManager _editorManager { get; set; }

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

        private void MouseMoveExecute(MouseEventArgs e)
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
            if (e.DeltaY > 0)
            {
                Zoom -= 0.1f;
            }
            else
            {
                Zoom += 0.1f;
            }
            Zoom = Math.Max(Zoom, 0.2f);
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
    }
}
