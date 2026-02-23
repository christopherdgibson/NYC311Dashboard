using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace NYC311Dashboard.Services.Contracts
{
    public interface ISidebarService
    {
        RenderFragment? CustomSidebar { get; set; }

        event Action? OnSidebarChanged;

        void SetSidebar(RenderFragment? fragment);

        RenderFragment RenderSidebarButton(string buttonText, string classes, string message, Func<Task>? onConfirm);

        public RenderFragment RenderInactiveSidebarButton(string buttonText, string message);

        public RenderFragment RenderCustomSidebar<TItem>(
            string label,
            IEnumerable<TItem> options,
            HashSet<TItem> selectedValues,
            EventCallback<HashSet<TItem>> selectedValuesChanged,
            Func<Task>? onSelectionChanged,
            bool inactive = false,
            Func<TItem, string>? optionLabel = null);

        Task ScrollToTop();

        Task ChangeClassName(string oldClassName, string newClassName);

        Task ToggleNavbar(string element = "nav ul", string newClassName = "nav-open");

        Task CloseNavOnClick(string element = "nav ul", string newClassName = "nav-open");
    }
}
