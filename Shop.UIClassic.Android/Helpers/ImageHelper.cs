namespace Shop.UIClassic.Android.Helpers
{
    using global::Android.Graphics;
    using System.Net;

    public class ImageHelper
    {
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }

}