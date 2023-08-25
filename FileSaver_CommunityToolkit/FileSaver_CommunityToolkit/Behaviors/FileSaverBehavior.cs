using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using Syncfusion.Maui.ImageEditor;

namespace FileSaver_CommunityToolkit
{
    public class FileSaverBehavior : Behavior<ContentPage>
    {
        private SfImageEditor imageEditor;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            imageEditor = bindable.FindByName<SfImageEditor>("imageEditor");
            imageEditor.ImageSaving += OnImageSaving;

        }
        private void OnImageSaving(object sender, ImageSavingEventArgs e)
        {
            SaveFile(CancellationToken.None, e.ImageStream);
        }
        async Task SaveFile(CancellationToken cancellationToken, Stream stream)
        {
            FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync("image.png", stream, cancellationToken);
            fileSaverResult.EnsureSuccess();
            await Toast.Make($"File is saved: {fileSaverResult.FilePath}").Show(cancellationToken);
        }
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            imageEditor.ImageSaving -= OnImageSaving;
        }
    }
}