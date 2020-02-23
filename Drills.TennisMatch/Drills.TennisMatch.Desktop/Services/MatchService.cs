using System;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Desktop.Services
{
    public interface IMatchService
    {
        Match Start();
    }

    public class MatchService: BaseService, IMatchService
    {
        public Match Start() => LiveMatch.Start(p1Serves: true);
    }
}
