namespace Payment_Backend_Service.Common
{

    public static class Enums
    {
        public enum Department
        {
            IT,
            HR,
            Finance,
            Marketing,
            Operations
        }
        public enum RequestType
        {
            Group,
            Individual
        }


        public enum Status
        {
            Approved,
            Rejected,
            AwaitingApproval,
            NotInitiated,
        }
        public enum Roles
        {
            Manager,
            Accountant

        }

    }
}
