using Avalonia;
using Avalonia.Markup.Xaml;

namespace KPAvaloniaApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
