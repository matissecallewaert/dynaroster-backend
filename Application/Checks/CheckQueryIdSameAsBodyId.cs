namespace Application.Checks;

public class CheckQueryIdSameAsBodyId
{
    public static bool CheckIdSameAsBodyId(Guid queryId, Guid bodyId)
    {
        return queryId == bodyId;
    }
}