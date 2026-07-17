using LifeQuest.Data;
using LifeQuest.Models;
using LifeQuest.Services;

const string cs = @"Server=.\SQLEXPRESS;Database=LifeQuest;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";

int failures = 0;
void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

var db = new Database(cs);
db.Initialize();
Check("Database.Initialize", true);

var users = new UserRepository(db);
var quests = new QuestRepository(db);

string uname = "smoke_" + Guid.NewGuid().ToString("N")[..8];
var u = users.Create(uname, "pass1234");
u = users.GetById(u.Id)!;
Check("Create+GetById", u != null && u.Username == uname);
Check("Password hashed (not plaintext)", PasswordHasher.Verify("pass1234", u!.PasswordHash) && u.PasswordHash != "pass1234");
Check("Wrong password rejected", !PasswordHasher.Verify("nope", u.PasswordHash));
Check("Exists()", users.Exists(uname));

var admin = users.Exists("hlib") ? users.GetByUsername("hlib") : users.Create("hlib_" + uname, "x", "uk");
if (!users.Exists("hlib"))
{
    var h = users.Create("hlib", "adminpass");
    Check("hlib is admin", users.GetById(h.Id)!.IsAdmin);
}
else Check("hlib is admin", users.GetByUsername("hlib")!.IsAdmin);

string day = DateTime.Now.ToString("yyyy-MM-dd");
var q = quests.Create(u.Id, "Smoke quest", "desc", "medium", day);
Check("Quest create", quests.Get(q.Id) != null);

q = quests.Get(q.Id)!;
q.Status = "pending"; q.HasProof = true; q.ProofPath = "x.png";
quests.Update(q);
var queue = quests.PendingQueue();
Check("PendingQueue has quest", queue.Exists(i => i.QuestId == q.Id));

int expected = GameLogic.ComputeXp("medium", true, u.Profile.Multiplier);
Check("ComputeXp medium+proof = 50", expected == 50);
var lvl = GameLogic.AddXp(u.Profile, expected);
u.Profile.QuestsDone++; u.Profile.ProofsApproved++;
q.Status = "done"; q.AwardedXp = expected;
quests.Update(q);
users.Update(u);

var reloaded = users.GetById(u.Id)!;
Check("XP persisted", reloaded.Profile.TotalXp == expected);
Check("QuestsDone persisted", reloaded.Profile.QuestsDone == 1);
Check("ProofsApproved persisted", reloaded.Profile.ProofsApproved == 1);

int noProof = GameLogic.ComputeXp("easy", false, 1.0);
Check("ComputeXp easy no-proof = 8", noProof == 8);

Check("XpForLevel(1)=100", GameLogic.XpForLevel(1) == 100);
Check("Rank lvl1 Novice", GameLogic.RankForLevel(1) == "Novice");
Check("Rank lvl11 Apprentice", GameLogic.RankForLevel(11) == "Apprentice");

var p = new Profile { Level = 100 };
Check("Prestige at max", GameLogic.Prestige(p) && p.Prestige == 1 && p.Level == 1 && Math.Abs(p.Multiplier - 1.25) < 0.001);

var lb = users.Leaderboard();
Check("Leaderboard returns rows", lb.Count > 0);

quests.Delete(q.Id);
Check("Quest delete", quests.Get(q.Id) == null);

Console.WriteLine();
Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURE(S)");
Environment.Exit(failures == 0 ? 0 : 1);
