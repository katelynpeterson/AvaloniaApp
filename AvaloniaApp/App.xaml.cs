using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloniaApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}