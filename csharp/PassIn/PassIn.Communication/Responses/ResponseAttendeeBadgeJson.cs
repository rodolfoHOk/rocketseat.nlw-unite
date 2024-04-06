namespace PassIn.Communication.Responses;

public class ResponseAttendeeBadgeJson
{
  public AttendeeBadgeDTO badge { get; set; }
}

public class AttendeeBadgeDTO
{
  public Guid Id { get; set; }
  public String Name { get; set; } = string.Empty;
  public String Email { get; set; } = string.Empty;
  public String? EventTitle { get; set; } = string.Empty;
  public String? CheckInURL { get; set; } = string.Empty;
}
