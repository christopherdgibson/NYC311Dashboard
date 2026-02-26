using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NYC311Dashboard.Components;
using NYC311Dashboard.Services.Contracts;

namespace NYC311Dashboard.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IJSRuntime _js;
        private readonly ILoadingService _loadingService;
        private readonly IMessagingService _messagingService;

        public RenderFragment? CustomSidebar { get; private set; }

        public LayoutService(IJSRuntime js, ILoadingService loadingService, IMessagingService messagingService)
        {
            _js = js;
            _loadingService = loadingService;
            _messagingService = messagingService;
        }

        public string? Title { get; private set; }

        public event Action? OnSidebarChanged;

        public void SetTitle(string? title)
        {
            Title = title;
            OnSidebarChanged?.Invoke(); // reuse existing event
        }

        public void SetSidebar(RenderFragment? fragment)
        {
            CustomSidebar = fragment;
            OnSidebarChanged?.Invoke();
        }
        public RenderFragment RenderInactiveSidebarButton(string buttonText, string message) => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "class", "sidebar-btn");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => _messagingService.ShowErrorDialog(message)));
            builder.AddContent(3, buttonText);
            builder.CloseElement();
        };

        public RenderFragment RenderSidebarButton(string buttonText, string classes, string message, Func<Task>? onConfirm) => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "class", classes);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => _messagingService.ShowDialog(message, onConfirm)));
            builder.AddContent(3, buttonText);
            builder.CloseElement();
        };

        public RenderFragment RenderCustomSidebar<TItem>(
            string label,
            IEnumerable<TItem> options,
            HashSet<TItem> selectedValues,
            EventCallback<HashSet<TItem>> selectedValuesChanged,
            Func<Task>? onSelectionChanged,
            bool inactive = false,
            Func<TItem, string>? optionLabel = null)
        {
            _loadingService.LoadingMessage = "I'm loading here!";
            _loadingService.IsLoading = true;
            try
            {
                RenderFragment? customSidebar = builder =>
                {
                    builder.OpenComponent(0, typeof(CheckboxDropdown<TItem>));
                    builder.AddAttribute(1, "Label", label);
                    builder.AddAttribute(2, "Options", options);
                    builder.AddAttribute(3, "SelectedValues", selectedValues);
                    builder.AddAttribute(4, "SelectedValuesChanged", selectedValuesChanged);
                    builder.AddAttribute(5, "OnSelectionChanged", onSelectionChanged);
                    builder.AddAttribute(6, "OptionLabel", optionLabel ?? (x => x?.ToString()));
                    builder.CloseComponent();
                };

                CustomSidebar = customSidebar;

                return customSidebar;
            }
            catch
            {
                _messagingService.ShowError("An error occurred. Please try again.!");
                return CustomSidebar; // Result.Failure("An error occurred. Please try again.!");
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }

        public async Task ChangeClassName(string oldClassName, string newClassName)
        {
            await _js.InvokeVoidAsync("changeClassName", oldClassName, newClassName);
        }

        public async Task ToggleNavbar(string element = "nav ul", string newClassName = "nav-open")
        {
            await _js.InvokeVoidAsync("toggleNav");
        }

        public async Task CloseNavOnClick(string element = "nav ul", string newClassName = "nav-open")
        {
            await _js.InvokeVoidAsync("closeNavOnClick");
        }

        public async Task ScrollToTop()
        {
            await _js.InvokeVoidAsync("scrollToTop");
        }
    }
}
