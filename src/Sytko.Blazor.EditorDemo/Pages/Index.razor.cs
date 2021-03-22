﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.Editor;
using Sytko.Blazor.Editor.Common;
using Sytko.Blazor.EditorDemo.Models;

namespace Sytko.Blazor.EditorDemo.Pages
{
    public partial class Index
    {
        [Inject]
        public ILogger<Index> _logger { get; set; }

        private EditorView EditorView;

        private List<DragItem> _items = new();

        private List<CatalogItem> _catalogItems = new();
        public CatalogItem DragImage { get; set; }

        private DragItem _selectedItem;
        public DragItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                _selectedArticleDragItem = value as ArticleDragItem;
                CalculateDetailPosition();
            }
        }

        private ArticleDragItem _selectedArticleDragItem;


        public Vector2Int DetailPosition { get; set; }


        protected override void OnInitialized()
        {
            _catalogItems.Add(new CatalogItem
            {
                ImageUrl = "/assets/tt/1016511_21905_7.webp"
            });
            _catalogItems.Add(new CatalogItem
            {
                ImageUrl = "/assets/tt/1026338_10302_7.webp"
            });
            _catalogItems.Add(new CatalogItem
            {
                ImageUrl = "/assets/tt/1025433_16396_7.webp"
            });

            _items.Add(new ArticleDragItem
            {
                X = -150,
                Y = 141,
                Width = 110,
                Height = 120,
                ImageUrl = "/assets/tt/1016511_21905_7.webp",
                BackgroundColor = "#00ffff00",
                AvailableArticles = new ArticleInformation[]
                {
                    new ArticleInformation
                    {
                        ColorHexCode = "#40444a",
                        ImageUrl = "/assets/tt/1016511_21905_7.webp"
                    },
                    new ArticleInformation
                    {
                        ColorHexCode = "#ce601c",
                        ImageUrl = "/assets/tt/1016979_21202_7.webp"
                    }
                }
            });

            _items.Add(new ArticleDragItem
            {
                X = 84,
                Y = 104,
                Width = 188,
                Height = 291,
                ImageUrl = "/assets/tt/1026338_10302_7.webp",
                BackgroundColor = "#00000000",
                AvailableArticles = new ArticleInformation[]
                {
                    new ArticleInformation
                    {
                        ColorHexCode = "#6a6461",
                        ImageUrl = "/assets/tt/1026345_10952_7.webp"
                    },
                    new ArticleInformation
                    {
                        ColorHexCode = "#091624",
                        ImageUrl = "/assets/tt/1026338_10302_7.webp"
                    },
                    new ArticleInformation
                    {
                        ColorHexCode = "#bfad9f",
                        ImageUrl = "/assets/tt/1024601_24371_7.webp"
                    }
                }
            });

            _items.Add(new ArticleDragItem
            {
                X = 94,
                Y = -95,
                Width = 165,
                Height = 216,
                ImageUrl = "/assets/tt/1025433_16396_7.webp",
                BackgroundColor = "#00000000",
                AvailableArticles = new ArticleInformation[]
                {
                    new ArticleInformation
                    {
                        ColorHexCode = "#879bb9",
                        ImageUrl = "/assets/tt/1025433_16396_7.webp"
                    },
                    new ArticleInformation
                    {
                        ColorHexCode = "#404354",
                        ImageUrl = "/assets/tt/1027028_19024_7.webp"
                    },
                    new ArticleInformation
                    {
                        ColorHexCode = "#626575",
                        ImageUrl = "/assets/tt/1023906_13804_7.webp"
                    }
                }
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
            _items.Add(new ArticleDragItem
            {
                X = (int)matrixPos.X - 83,
                Y = (int)matrixPos.Y - 108,
                Width = 165,
                Height = 216,
                ImageUrl = DragImage.ImageUrl,
                BackgroundColor = "#00000000",
            });
            SelectedItem = _items.LastOrDefault();
        }
    }
}
