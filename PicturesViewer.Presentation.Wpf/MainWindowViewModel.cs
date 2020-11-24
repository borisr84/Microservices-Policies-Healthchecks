using Newtonsoft.Json;
using PicturesCommon;
using Polly;
using Polly.Fallback;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PicturesViewer.Presentation.Wpf
{
    class MainWindowViewModel : BindableBase
    {
        private HttpClient _httpClient = new HttpClient();

        private readonly IList<string> _dataSourceUrls = new List<string>
        {
            "https://localhost:44309", //Local pictures
            "https://localhost:44332" //Remote pictures
        };
        private IEnumerator<string> _urlEnumerator;

        private readonly AsyncFallbackPolicy<IList<Picture>> _fallbackPolicy;

        private IList<Picture> _pictures;
        public IList<Picture> Pictures
        {
            get => _pictures;
            set => SetProperty(ref _pictures, value);
        }

        public MainWindowViewModel()
        {
            _fallbackPolicy = Policy<IList<Picture>>.Handle<HttpRequestException>()
            .FallbackAsync(fallbackAction: async (ct) => {
                if (_urlEnumerator.MoveNext())
                    return await GetData(_urlEnumerator.Current);

                return null;
            });
        }

        private ICommand _showPicturesCommand;
        public ICommand ShowPicturesCommand => _showPicturesCommand ??= _showPicturesCommand = new DelegateCommand(async () =>
        {
            _urlEnumerator = _dataSourceUrls.GetEnumerator();
            _urlEnumerator.MoveNext();
            Pictures = new List<Picture>(await _fallbackPolicy.ExecuteAsync(async () => 
                await GetData(_urlEnumerator.Current)));
        });

        private async Task<IList<Picture>> GetData(string url)
        {
            var response = await _httpClient.GetAsync($"{url}/api/Pictures");
            var payload = JsonConvert.DeserializeObject<IList<Picture>>(await response.Content.ReadAsStringAsync());
            return payload;
        }
    }
}
