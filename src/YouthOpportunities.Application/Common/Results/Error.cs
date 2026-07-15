namespace YouthOpportunities.Application.Common.Results
{
    public sealed record Error(
        string Code,
        string Message,
        ResultStatus Status)
    {
        public static readonly Error None = new(
            string.Empty,
            string.Empty,
            ResultStatus.Success);
    }
}
