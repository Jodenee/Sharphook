namespace Sharphook.Utility.Enums;

public enum PublicUserFlag : int
{
	Staff = 1 << 0,
	Partner = 1 << 1,
	Hypesquad = 1 << 2,
	BugHunterLevelOne = 1 << 3,
	HypesquadHouseOfBravery = 1 << 6,
	HypesquadHouseOfBrilliance = 1 << 7,
	HypesquadHouseOfBalance = 1 << 8,
	PremiumEarlySupporter = 1 << 9,
	BugHunterLevelTwo = 1 << 14,
	VerifiedBot = 1 << 16,
	VerifiedDeveloper = 1 << 17,
	CertifiedModerator = 1 << 18,
	BotHttpInteractions = 1 << 19,
	ActiveDeveloper = 1 << 22
}
