using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.Editor.Components
{
    public partial class DragItemSelector
    {
        [Parameter]
        public EditorView Editor { get; set; }

        private DragItem _selectedItem;
        [Parameter]
        public DragItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RefreshPositionAndSize();
            }
        }

        private Rectangle _positionAndSize = Rectangle.Empty;

        private void RefreshPositionAndSize()
        {
            if (_selectedItem == null)
            {
                _positionAndSize = Rectangle.Empty;
                return;
            }

            _positionAndSize = Editor.ConvertRectangleFromMatrixToWorld(new Rectangle(_selectedItem.X - 1, _selectedItem.Y - 1, _selectedItem.Width + 2, _selectedItem.Height + 2));
        }

    }
}
