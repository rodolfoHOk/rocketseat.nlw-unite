namespace PassIn.Communication.Responses;
public class ResponseAttendeesListJson
{
  public List<ResponseAttendeeJson> Attendees { get; set; } = [];
  public int Total { get; set; }
}
