﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.Editor.Common;

namespace Sytko.Blazor.Editor.Components
{
    public partial class DragItemSelector
    {
        [Inject]
        public ILogger<DragItemSelector> _logger { get; set; }

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

            var newRect = Editor.ConvertRectangleFromMatrixToWorld(new Rectangle(_selectedItem.X, _selectedItem.Y, _selectedItem.Width, _selectedItem.Height));
            _positionAndSize = new Rectangle(newRect.X - 1, newRect.Y - 1, newRect.Width + 2, newRect.Height + 2);
        }

        private void MouseDownExecute(MouseEventArgs e)
        {
            Editor.StartResizeItem(e);
        }

    }
}
