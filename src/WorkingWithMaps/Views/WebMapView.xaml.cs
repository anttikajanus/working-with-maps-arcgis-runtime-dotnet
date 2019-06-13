using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Mapping.Popups;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkingWithMaps.Example.Views
{
    /// <summary>
    /// Interaction logic for WebMapView.xaml
    /// </summary>
    public partial class WebMapView : UserControl
    {
        public WebMapView()
        {
            InitializeComponent();
        }

        private RuntimeImage InfoIcon { get; } = new RuntimeImage(new Uri("pack://application:,,,/Resources/Images/baseline_fullscreen_black_18dp.png"));

        private async void MapView_GeoViewTapped(object sender, GeoViewInputEventArgs e)
        {
            try
            {
                var result = await mapView.IdentifyLayersAsync(e.Position, 3, false);

                // Retrieves or builds Popup from IdentifyLayerResult
                var popup = GetPopup(result);

                // Displays callout and updates visibility of PopupViewer
                if (popup != null)
                {
                    var callout = new CalloutDefinition(popup.GeoElement)
                    {
                        Tag = popup,
                        ButtonImage = InfoIcon,
                        OnButtonClick = new Action<object>((s) =>
                        {
                            popupPane.Visibility = Visibility.Visible;
                            popupBox.Visibility = Visibility.Visible;
                            popupViewer.Visibility = Visibility.Visible;
                            popupViewer.PopupManager = new PopupManager(s as Popup);
                        })
                    };
                    if (popupBox.Visibility == Visibility.Visible)
                    {
                       
                        popupViewer.PopupManager = new PopupManager(popup);
                        popupViewer.PopupManager.StartEditing();
                    }
                    mapView.ShowCalloutForGeoElement(popup.GeoElement, e.Position, callout);
                }
                else
                {
                    mapView.DismissCallout();
                    popupViewer.PopupManager = null;
                    popupViewer.Visibility = Visibility.Collapsed;
                    popupBox.Visibility = Visibility.Collapsed;
                    popupPane.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
        }

        private Popup GetPopup(IdentifyLayerResult result)
        {
            if (result == null)
            {
                return null;
            }

            var popup = result.Popups.FirstOrDefault();
            if (popup != null)
            {
                return popup;
            }

            var geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                if (result.LayerContent is IPopupSource)
                {
                    var popupDefinition = ((IPopupSource)result.LayerContent).PopupDefinition;
                    if (popupDefinition != null)
                    {
                        return new Popup(geoElement, popupDefinition);
                    }
                }

                return Popup.FromGeoElement(geoElement);
            }

            return null;
        }

        private Popup GetPopup(IEnumerable<IdentifyLayerResult> results)
        {
            if (results == null)
            {
                return null;
            }
            foreach (var result in results)
            {
                var popup = GetPopup(result);
                if (popup != null)
                {
                    return popup;
                }

                foreach (var subResult in result.SublayerResults)
                {
                    popup = GetPopup(subResult);
                    if (popup != null)
                    {
                        return popup;
                    }
                }
            }

            return null;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (toolPane == null) return;
            if (bookmarkPane == null) return;

            if (e.AddedItems.Count > 0)
            {
                var selection = e.AddedItems[0] as ListBoxItem;
                switch (selection.Tag)
                {
                    case "Bookmarks":
                        if (bookmarkPane.Visibility == Visibility.Collapsed)
                        {
                            tocPane.Visibility = Visibility.Collapsed;
                            bookmarkPane.Visibility = Visibility.Visible;
                            toolPane.Visibility = Visibility.Visible;
                        }                      
                        break;
                    case "TOC":
                        if (tocPane.Visibility == Visibility.Collapsed)
                        {
                            bookmarkPane.Visibility = Visibility.Collapsed;
                            toolPane.Visibility = Visibility.Visible;
                            tocPane.Visibility = Visibility.Visible;
                        }
                        break;
                    default:
                        bookmarkPane.Visibility = Visibility.Collapsed;
                        toolPane.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var bookmark = e.AddedItems[0] as Bookmark;
                await mapView.SetViewpointAsync(bookmark.Viewpoint);
            }
        }
    }
}
