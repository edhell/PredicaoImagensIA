using Newtonsoft.Json;

namespace TrabalhoTestesSoftware
{
    public class Detections
    {
        /// <summary>
        /// Detects the scene in an image. 365 scenes in total
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns>ResponseScene</returns>
        public static async Task<ResponseScene?> detectionScene(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/scene", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                ResponseScene response = JsonConvert.DeserializeObject<ResponseScene>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO SCENE: " + ex.Message);
                return null;
            }
        }

        

        /// <summary>
        /// Detects objects in an image. 80 objects in total
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseObject?> detectionObject(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/detection", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                ResponseObject response = JsonConvert.DeserializeObject<ResponseObject>(jsonString);

                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO DETECTION: " + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Detects faces in an image.
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseFace?> detectionFace(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/face/recognize", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                ResponseFace response = JsonConvert.DeserializeObject<ResponseFace>(jsonString);

                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO FACE: " + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Elarge a image in 4x with IA.
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseEnhance?> enhanceObject(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/enhance", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                ResponseEnhance response = JsonConvert.DeserializeObject<ResponseEnhance>(jsonString);

                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Enhance: " + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Detects if two faces matches.
        /// </summary>
        /// <param name="image1_path"></param>
        /// <param name="image2_path"></param>
        /// <returns></returns>
        public static async Task<ResponseMatchFace?> detectionMatchFace(string image1_path, string image2_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new MultipartFormDataContent();
                var image_data1 = File.OpenRead(image1_path);
                var image_data2 = File.OpenRead(image2_path);
                request.Add(new StreamContent(image_data1), "image1", Path.GetFileName(image1_path));
                request.Add(new StreamContent(image_data2), "image2", Path.GetFileName(image2_path));
                var output = await client.PostAsync("http://192.168.15.11:82/v1/vision/face/match", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseMatchFace>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Detection Match Face: " + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Detects the license plate in an image.
        /// </summary>
        /// <param name="image1_path"></param>
        /// <param name="image2_path"></param>
        /// <returns></returns>
        public static async Task<ResponsePlate?> detectionPlate(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/custom/licence-plate", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePlate>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Plate: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Detects objects in an image with less light or night. optimal for a segurity camera
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseObjectDark?> detectionObjectDark(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/custom/dark", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseObjectDark>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO Plate: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Detects logos in an image. Like FedEx and etc...
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseOpenlogo?> detectionOpenlogo(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/custom/openlogo", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseOpenlogo>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO OpenLogo: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Detects human actions in an image. Like exercite and etc...
        /// </summary>
        /// <param name="image_path"></param>
        /// <returns></returns>
        public static async Task<ResponseActionNet?> detectionActionNet(string image_path)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var request = new MultipartFormDataContent();
                var image_data = File.OpenRead(image_path);
                request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
                var output = await client.PostAsync("http://192.168.15.10:82/v1/vision/custom/actionnetv2", request);
                var jsonString = await output.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseActionNet>(jsonString);

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO ActionNet: " + ex.Message);
                return null;
            }

        }


        /*
        static async Task recognizeFaceAndSplitImage(string image_path)
        {
            HttpClient client = new HttpClient();
            var request = new MultipartFormDataContent();
            var image_data = File.OpenRead(image_path);
            request.Add(new StreamContent(image_data), "image", Path.GetFileName(image_path));
            var output = await client.PostAsync("http://192.168.10:82/v1/vision/detection", request);
            var jsonString = await output.Content.ReadAsStringAsync();
            Response response = JsonConvert.DeserializeObject<Response>(jsonString);

            var i = 0;

            foreach (var user in response.predictions)
            {

                var width = user.x_max - user.x_min;
                var height = user.y_max - user.y_min;

                var crop_region = new SixLabors.ImageSharp.Rectangle(user.x_min, user.y_min, width, height);

                using (var image = SixLabors.ImageSharp.Image.Load(image_path))
                {

                    image.Mutate(x => x.Crop(crop_region));
                    image.Save(user.label + i.ToString() + "_.jpg");

                }

                i++;

            }

        }*/

    }


    public class ResponseObject
    {
        public bool success { get; set; }
        public Predict[] predictions { get; set; }

    }
     
    public class Predict
    {
        public string label { get; set; }
        public float confidence { get; set; }
        public int y_min { get; set; }
        public int x_min { get; set; }
        public int y_max { get; set; }
        public int x_max { get; set; }

    }
     
    public class ResponseScene
    {
        public bool success { get; set; }
        public double confidence { get; set; }
        public string label { get; set; }
        public int duration { get; set; }
    }
     
    public class ResponseEnhance
    {
        public bool success { get; set; }
        public String base64 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
     
    public class ResponseFace
    {
        public bool success { get; set; }
        public List<object> predictions { get; set; }
        public int duration { get; set; }
    }
     
    public class ResponseMatchFace
    {
        /*public bool success { get; set; }
        public double confidence { get; set; }
        public string label { get; set; }
        public int duration { get; set; }*/
    }

    public class ResponsePlate
    {
        public bool success { get; set; }
        public Predict[] predictions { get; set; }
    }

    public class ResponseObjectDark
    {
        public bool success { get; set; }
        public Predict[] predictions { get; set; }
    }

    public class ResponseOpenlogo
    {
        public bool success { get; set; }
        public Predict[] predictions { get; set; }
    }

    public class ResponseActionNet
    {
        public bool success { get; set; }
        public Predict[] predictions { get; set; }
    }


}
