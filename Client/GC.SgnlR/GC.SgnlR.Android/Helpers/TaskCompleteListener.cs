using Android.Gms.Tasks;
using System.Threading.Tasks;

namespace GC.SgnlR.Droid.Helpers
{
    internal class TaskCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        private readonly TaskCompletionSource<Java.Lang.Object> _taskCompletionSource;

        public TaskCompleteListener(TaskCompletionSource<Java.Lang.Object> tcs)
        {
            _taskCompletionSource = tcs;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsCanceled)
            {
                _taskCompletionSource.SetCanceled();
            }
            else if (task.IsSuccessful)
            {
                _taskCompletionSource.SetResult(task.Result);
            }
            else
            {
                _taskCompletionSource.SetException(task.Exception);
            }
        }
    }

    public static class TaskExtensions
    {
        public static Task<Java.Lang.Object> ToAwaitableTask(this Android.Gms.Tasks.Task task)
        {
            var taskCompletionSource = new TaskCompletionSource<Java.Lang.Object>();
            var taskCompleteListener = new TaskCompleteListener(taskCompletionSource);
            task.AddOnCompleteListener(taskCompleteListener);

            return taskCompletionSource.Task;
        }
    }
}