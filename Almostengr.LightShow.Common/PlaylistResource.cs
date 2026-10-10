namespace Almostengr.LightShow.Common;

public sealed class PlaylistResource : ApiResource
{
    public IList<ShowSequence> Sequences { get; set; }
    public int UserId { get; set; }

    public class ShowSequence
    {
        public string SequenceFileName { get; set; }
        public string SongFileName { get; set; }
        public int DisplayId { get; set; }
    }
}
