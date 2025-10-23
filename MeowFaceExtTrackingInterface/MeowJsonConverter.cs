using Newtonsoft.Json;

namespace MeowFaceExtTrackingInterface
{
    public class MeowShapeListConverter : JsonConverter<MeowShape[]>
    {
        public override MeowShape[] ReadJson(JsonReader reader, Type objectType, MeowShape[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Convert JSON to JArray
            MeowNamedShape[] namedShapes = serializer.Deserialize<MeowNamedShape[]>(reader) ?? Array.Empty<MeowNamedShape>();

            // Initialize array with the number of enum values
            MeowShape[] blendShapes = new MeowShape[Enum.GetValues(typeof(MeowShapeType)).Length];
            Array.Fill(blendShapes, new MeowShape()); // Initialize array values

            foreach (MeowNamedShape namedShape in namedShapes)
            {
                // Try to convert the string to an enum
                if (Enum.TryParse(namedShape.k, true, out MeowShapeType shapeType))
                {
                    // Store the value at the enum index position
                    blendShapes[(int)shapeType].v = namedShape.v;
                }
            }

            return blendShapes;
        }

        public override void WriteJson(JsonWriter writer, MeowShape[]? value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Serialization not implemented.");
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}
