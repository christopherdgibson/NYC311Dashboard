using Microsoft.AspNetCore.Components;

namespace NYC311Dashboard.Services.Contracts
{
    public interface ISidebarService
    {
        RenderFragment? CustomSidebar { get; set; }

        event Action? OnSidebarChanged;

        void SetSidebar(RenderFragment? fragment);

        RenderFragment RenderSidebarButton(string buttonText, string classes, EventCallback onClick);

        public RenderFragment RenderCustomSidebar<TItem>(
            string label,
            IEnumerable<TItem> options,
            HashSet<TItem> selectedValues,
            EventCallback<HashSet<TItem>> selectedValuesChanged,
            Func<Task>? onSelectionChanged,
            bool inactive = false,
            Func<TItem, string>? optionLabel = null);

        Task ScrollToTop();
    }
}
