using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.Editor;
using Sytko.Blazor.Editor.Common;

namespace Sytko.Blazor.EditorDemo.Pages
{
    public partial class Index
    {
        [Inject]
        public ILogger<Index> _logger { get; set; }

        private EditorView EditorView;

        private List<DragItem> _items = new();

        private DragItem _selectedItem;
        public DragItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                CalculateDetailPosition();
            }
        }

        

        public Vector2Int DetailPosition { get; set; }
        

        protected override void OnInitialized()
        {
            _items.Add(new DragItem
            {
                X = 41,
                Y = 147,
                Width = 221,
                Height = 240,
                ImageUrl = "/assets/tt/1016511_21905_7.webp",
                BackgroundColor = "#00ffff00"
            });

            _items.Add(new DragItem
            {
                X = -13,
                Y = -260,
                Width = 330,
                Height = 432,
                ImageUrl = "/assets/tt/1025433_16396_7.webp",
                BackgroundColor = "#00000000"
            });
        }


        private void CalculateDetailPosition()
        {
            if (_selectedItem == null)
            {
                return;
            }

            
            var worldPosition = EditorView.ConvertRectangleFromMatrixToWorld(new Rectangle(_selectedItem.X, _selectedItem.Y, _selectedItem.Width, _selectedItem.Height));

            var halfWidth = worldPosition.Width / 2;
            var halfHeight = worldPosition.Height / 2;
            var maxLength = (int)Math.Sqrt((halfWidth * halfWidth) + (halfHeight * halfHeight));

            DetailPosition = new Vector2Int(worldPosition.X + halfWidth + maxLength + 2, worldPosition.Y + halfHeight);
        }

        private void OnCurrentActionChanged(EditorActionTypes newAction)
        {
            //_logger.LogError(newAction.ToString());
            CalculateDetailPosition();
        }

        protected void AddObjectExecute()
        {
            _logger.LogInformation("BLUBB");
            _items.Add(new DragItem
            {
                X = -200,
                Y = 200,
                Width = 200,
                Height = 300,
                ImageUrl = "https://images.ctfassets.net/wmdwnw6l5vg5/71jz89dFBIdA9KHrLh8T0h/c4c0a817afe77c35ff5a1461f052b03f/economy.png",
                BackgroundColor = "#81a0ff"
            });
        }

        private void RemoveSelectedItem()
        {
            _items.Remove(SelectedItem);
            SelectedItem = null;
        }

        private void RotateSelectedItem()
        {
            SelectedItem.Rotation += 20;
        }

        private void ScaleAddSelectedItem()
        {
            SelectedItem.Width += 20;
            SelectedItem.Height += 20;
        }
    }
}
