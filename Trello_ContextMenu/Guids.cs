// Guids.cs
// MUST match guids.h
using System;

namespace Inayathullah.Trello_ContextMenu
{
    static class GuidList
    {
        public const string guidTrello_ContextMenuPkgString = "1f750350-b733-4f9b-82cc-5d0c4616ace9";
        public const string guidTrello_ContextMenuCmdSetString = "c578efbc-adee-4575-811f-ae42f287dfd5";
        public const string guidToolWindowPersistanceString = "ab229e8f-1d41-4dd7-851b-da56ea9cb265";

        public static readonly Guid guidTrello_ContextMenuCmdSet = new Guid(guidTrello_ContextMenuCmdSetString);
    };
}