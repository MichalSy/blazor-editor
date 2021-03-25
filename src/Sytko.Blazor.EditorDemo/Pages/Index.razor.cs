using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sytko.Blazor.Editor;
using Sytko.Blazor.Editor.Common;
using Sytko.Blazor.Editor.Models;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Pages
{
    public partial class Index
    {
        [Inject]
        public ILogger<Index> _logger { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        public string ImageBasePath => Configuration["DefaultImageBasePath"];

        private EditorView EditorView;

        private List<DragItem> _items = new();

        
        public ArticleInformation DragImage { get; set; }

        private DragItemWithModel<ArticleInformation> _selectedItem;
        public DragItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value as DragItemWithModel<ArticleInformation>;
                CalculateDetailPosition();
            }
        }

        public Vector2Int DetailPosition { get; set; }


        protected override void OnInitialized()
        {
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
            CalculateDetailPosition();
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

        private void SwitchArticleForSelectedItem(ArticleInformation article)
        {
            SelectedItem.ImageUrl = article.ImageUrl;
        }

        private void HandleDrop(DragEventArgs e)
        {
            _logger.LogInformation($"{DragImage.ImageUrl}");
            var matrixPos = EditorView.ConvertPositionFromWorldToMatrix(new Vector2Int((int)e.OffsetX, (int)e.OffsetY));
            var newModel = new DragItemWithModel<ArticleInformation>
            {
                X = (int)matrixPos.X - 83,
                Y = (int)matrixPos.Y - 108,
                Width = 165,
                Height = 216,
                ImageUrl = DragImage.ImageUrl,
                BackgroundColor = "#00000000",
                DataModel = DragImage
            };
            _items.Add(newModel);
            SelectedItem = newModel;
        }
    }
}
