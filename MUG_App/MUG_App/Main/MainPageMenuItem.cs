using System;
using MUG_App.Group;

namespace MUG_App.Main
{

    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
            TargetType = typeof(GroupPage);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
