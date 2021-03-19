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


        private bool _isDragActive = false;
        private Vector2Int _dragMouseStartOffsetToItem = Vector2Int.Zero;

        private bool _isMoveActive = false;
        private Vector2Int _moveMouseStartOffset = Vector2Int.Zero;



        public Vector2Int LocalMousePointer = Vector2Int.Zero;

        public float Zoom { get; set; } = 1.0f;


        public Vector2Int ViewportPosition { get; set; } = new Vector2Int(500, 300);


        protected override void OnInitialized()
        {
            _logger.LogInformation($"Init EditorView");
        }

        internal Vector2Int ConvertPositionFromWorldToMatrix(Vector2Int point)
        {
            var posX = (point.X - ViewportPosition.X) / Zoom;
            var posY = (point.Y - ViewportPosition.Y) / Zoom;
            return new Vector2Int((int)posX, (int)posY);
        }

        internal Rectangle ConvertRectangleFromMatrixToWorld(Rectangle rectangle)
        {
            _logger.LogInformation($"{rectangle.X}, {rectangle.Y}, {rectangle.Width}, {rectangle.Height}, ");
            var posX = Math.Floor(ViewportPosition.X + (rectangle.X * Zoom));
            var posY = Math.Floor(ViewportPosition.Y + (rectangle.Y * Zoom));
            var width = Math.Ceiling(rectangle.Width * Zoom);
            var height = Math.Ceiling(rectangle.Width * Zoom);

            return new Rectangle((int)posX, (int)posY, (int)width, (int)height);
        }

        private void MouseMoveExecute(MouseEventArgs e)
        {
            var localMouse = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            LocalMousePointer = localMouse;

            //_logger.LogInformation($"Mouse: {localMouse.X}, {localMouse.Y}");

            if (_isDragActive)
            {
                if (SelectedItem == null)
                {
                    return;
                }

                SelectedItem.X = (int)((localMouse.X - _dragMouseStartOffsetToItem.X));
                SelectedItem.Y = (int)((localMouse.Y - _dragMouseStartOffsetToItem.Y));

                //SelectedItem.X = (int)Math.Round(SelectedItem.X / 10f, 0) * 10;
                //SelectedItem.Y = (int)Math.Round(SelectedItem.Y / 10f, 0) * 10;

            }
            else if (_isMoveActive)
            {
                ViewportPosition = new Vector2Int((int)(e.OffsetX - _moveMouseStartOffset.X), (int)(e.OffsetY - _moveMouseStartOffset.Y));
            }

        }

        private async void MouseDownExecute(MouseEventArgs e)
        {
            if (_isDragActive)
            {
                return;
            }

            _isDragActive = false;
            _isMoveActive = true;
            SelectedItem = null;
            await SelectedItemChanged.InvokeAsync(SelectedItem);

            _moveMouseStartOffset = new Vector2Int((int)((float)e.OffsetX - ViewportPosition.X), (int)((float)e.OffsetY - ViewportPosition.Y));
            _logger.LogInformation($"IsDragActve: {_isDragActive}, IsMoveActve: {_isMoveActive}, Mouse: {e.OffsetX}, {e.OffsetY}");
        }

        private async void MouseDownItemExecute(MouseEventArgs e, DragItem item)
        {
            if (_isMoveActive)
            {
                return;
            }

            _isDragActive = true;
            SelectedItem = item;
            Items.Remove(SelectedItem);
            Items.Add(SelectedItem);
            await SelectedItemChanged.InvokeAsync(SelectedItem);

            var localMouse = ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            _dragMouseStartOffsetToItem = new Vector2Int(localMouse.X - item.X, localMouse.Y - item.Y);
            _logger.LogInformation($"IsDragActve: {_isDragActive}, Mouse: {e.OffsetX}, {e.OffsetY}");
        }

        private void MouseUpItemExecute(MouseEventArgs e)
        {
            _isDragActive = false;
            _isMoveActive = false;
            _logger.LogInformation($"IsDragActve: {_isDragActive}, IsMoveActve: {_isMoveActive}");
        }

        private void MouseWheelExecute(WheelEventArgs e)
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
        }
    }
}
