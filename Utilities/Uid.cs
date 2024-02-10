namespace EmployeeManagementApp.Utilities
{
    public static class Uid
    {
        private static int currentId = 1;

        public static int NewUid()
        {
            return Interlocked.Increment(ref currentId);
        }
    }
}
