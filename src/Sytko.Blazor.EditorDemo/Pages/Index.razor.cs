using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.Editor;

namespace Sytko.Blazor.EditorDemo.Pages
{
    public partial class Index
    {
        [Inject]
        public ILogger<Index> _logger { get; set; }

        private List<DragItem> _items = new();

        public DragItem SelectedItem { get; set; }

        protected override void OnInitialized()
        {
            _items.Add(new DragItem
            {
                X = 0,
                Y = 0,
                Width = 200,
                Height = 200,
                ImageUrl = "https://images.ctfassets.net/wmdwnw6l5vg5/71jz89dFBIdA9KHrLh8T0h/c4c0a817afe77c35ff5a1461f052b03f/economy.png",
                BackgroundColor = "#ffff00"
            });

            _items.Add(new DragItem
            {
                X = -200,
                Y = -200,
                Width = 100,
                Height = 100,
                ImageUrl = "https://images.ctfassets.net/wmdwnw6l5vg5/71jz89dFBIdA9KHrLh8T0h/c4c0a817afe77c35ff5a1461f052b03f/economy.png",
                BackgroundColor = "#81a0ff"
            });
        }

        protected void AddObjectExecute()
        {
            _logger.LogInformation("BLUBB");
            _items.Add(new DragItem
            {
                X = -200,
                Y = 200,
                Width = 100,
                Height = 100,
                ImageUrl = "https://images.ctfassets.net/wmdwnw6l5vg5/71jz89dFBIdA9KHrLh8T0h/c4c0a817afe77c35ff5a1461f052b03f/economy.png",
                BackgroundColor = "#81a0ff"
            });
        }

        private void RemoveSelectedItem()
        {
            _items.Remove(SelectedItem);
            SelectedItem = null;
        }
    }
}
