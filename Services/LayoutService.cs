using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using NYC311Dashboard.Components;
using NYC311Dashboard.Services.Contracts;
using NYC311Dashboard.Services.Models;

namespace NYC311Dashboard.Services
{
    public class LayoutService : ILayoutService, IDisposable
    {
        private readonly NavigationManager _navigation;
        private readonly IJSRuntime _js;
        private readonly ILoadingService _loadingService;
        private readonly IMessagingService _messagingService;

        public LayoutService(NavigationManager navigation, IJSRuntime js, ILoadingService loadingService, IMessagingService messagingService)
        {
            _navigation = navigation;
            _js = js;
            _loadingService = loadingService;
            _messagingService = messagingService;

            _navigation.LocationChanged += OnNavigationChanged;
        }

        private List<Action> _closeHandlers = new();

        public string? MainTitle { get; private set; }
        public string? SupTitle { get; private set; }

        public RenderFragment? CustomSidebar { get; private set; }
        public event Action? OnSidebarChanged;
        public event Action? OnLocationChanged;
        public event Action? OnCloseAllDropdowns;

        public void SetTitle(string? mainTitle, string? supTitle = null)
        {
            MainTitle = mainTitle;
            SupTitle = supTitle;
            OnSidebarChanged?.Invoke(); // reuse existing event?
        }

        public void SetSidebar(RenderFragment? fragment)
        {
            CustomSidebar = fragment;
            OnSidebarChanged?.Invoke();
        }

        public RenderFragment RenderInactiveButton(string buttonText, string message) => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "class", "sidebar-btn");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => _messagingService.ShowErrorDialog(message)));
            builder.AddContent(3, buttonText);
            builder.CloseElement();
        };

        public RenderFragment RenderButton(string buttonText, string classes, string message, Func<Task>? onConfirm) => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "class", classes);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => _messagingService.ShowDialog(message, onConfirm)));
            builder.AddContent(3, buttonText);
            builder.CloseElement();
        };

        public RenderFragment RenderCheckboxDropdown<TItem>(CheckboxDropdownConfig<TItem> config, string header)
        {
            return RenderMultipleCheckboxDropdowns(new List<CheckboxDropdownConfig<TItem>> { config }, header);
        }

        public RenderFragment RenderMultipleCheckboxDropdowns<TItem>(IEnumerable<CheckboxDropdownConfig<TItem>> configs, string header)
        {
            _loadingService.IsLoading = true;
            try
            {
                if (configs is null)
                {
                    throw new ArgumentNullException(nameof(configs));
                }
                if (!configs.Any())
                {
                    return builder => { }; // silent fallback, already handled upstream
                }

                RenderFragment customSidebar = builder =>
                {
                    int seq = 0;

                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "class", "sidebar-section-header");
                    builder.AddContent(seq++, header);
                    builder.CloseElement();

                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "class", "sidebar-dropdown-stack");

                    foreach (var config in configs.OrderBy(c => c.Label))
                    {
                        builder.OpenComponent(seq++, typeof(CheckboxDropdown<TItem>));
                        builder.AddAttribute(seq++, "Label", config.Label);
                        builder.AddAttribute(seq++, "Options", config.Options);
                        builder.AddAttribute(seq++, "SelectedValues", config.SelectedValues);
                        builder.AddAttribute(seq++, "SelectedValuesChanged", config.SelectedValuesChanged);
                        builder.AddAttribute(seq++, "OnSelectionChanged", config.OnSelectionChanged);
                        builder.AddAttribute(seq++, "OptionLabel", config.OptionLabel ?? (x => x?.ToString()));
                        builder.AddAttribute(seq++, "SetIndeterminateSelection", SetIndeterminate);
                        builder.AddAttribute(seq++, "RegisterCloseHandler", RegisterCloseHandler);
                        builder.CloseComponent();
                    }

                    builder.CloseElement();
                };

                CustomSidebar = customSidebar;
                return customSidebar;
            }
            catch
            {
                _messagingService.ShowError(Resources.messaging_service_error_occurred);
                return CustomSidebar;
            }
            finally
            {
                _loadingService.IsLoading = false;
            }
        }

        private async void OnNavigationChanged(object? sender, LocationChangedEventArgs e)
        {
            try
            {
                //SetTitle(null); if needed later
                await ScrollToTop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Join(" ", Resources.failed_navigation_handler, ex.Message));
            }

            OnLocationChanged?.Invoke();
        }

        public async Task ChangeClassName(string oldClassName, string newClassName)
        {
            await _js.InvokeVoidAsync("changeClassName", oldClassName, newClassName);
        }

        public async Task ToggleClassName(string element = "nav ul", string className = "nav-open")
        {
            await _js.InvokeVoidAsync("toggleClassName", element, className);
        }

        public async Task CloseNavOnClick(string element = "nav ul", string newClassName = "nav-open")
        {
            await _js.InvokeVoidAsync("closeNavOnClick");
        }

        public async Task ScrollToTop()
        {
            await _js.InvokeVoidAsync("scrollToTop");
        }

        public async Task CloseDropdownOnClickAway()
        {
            await _js.InvokeVoidAsync("closeDropdownOnClickAway", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void OnGlobalClick() => CloseAllDropdowns();

        private async Task SetIndeterminate(ElementReference selectAllRef, bool IsIndeterminate)
        {
            await _js.InvokeVoidAsync("setIndeterminateSelection", selectAllRef, IsIndeterminate);
        }

        private void RegisterCloseHandler(Action handler)
        {
            _closeHandlers.Add(handler);
        }

        private void CloseAllDropdowns()
        {
            foreach (var handler in _closeHandlers)
            {
                handler.Invoke();
            }
            OnCloseAllDropdowns.Invoke();
        }

        public void Dispose()
        {
            _navigation.LocationChanged -= OnNavigationChanged;
        }
    }
}
