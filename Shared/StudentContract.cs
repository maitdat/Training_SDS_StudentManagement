using System.Runtime.Serialization;

namespace Shared
{
    public interface IStudentService
    {
        //Task<> GetStudentById(int id);
    }


    [DataContract]
    public class a
    {
        [DataMember(Order = 1)]
        public string NewText { get; set; }

        [DataMember(Order = 2)]
        public int NewValue { get; set; }
    }

    [DataContract]
    public class n
    {
        [DataMember(Order = 1)]
        public string Text { get; set; }

        [DataMember(Order = 2)]
        public int Value { get; set; }
    }
}
