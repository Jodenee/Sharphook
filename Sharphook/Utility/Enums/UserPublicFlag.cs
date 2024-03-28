namespace Sharphook.Utility.Enums;

public enum UserPublicFlag : int
{
    Staff = 1 << 0,
    Partner = 1 << 1,
    Hypesquad = 1 << 2,
    BugHunterLevelOne = 1 << 3,
    HypesquadHouseOfBravery = 1 << 4,
    HypesquadHouseOfBrilliance = 1 << 5,
    HypesquadHouseOfBalance = 1 << 6,
    PremiumEarlySupporter = 1 << 7,
    TeamPseudoUser = 1 << 8,
    EarlySupporter = 1 << 9,
    BugHunterLevelTwo = 1 << 14,
    VerifiedBot = 1 << 16,
    VerifiedDeveloper = 1 << 17,
    CertifiedModerator = 1 << 18,
    BotHttpInteractions = 1 << 19,
    ActiveDeveloper = 1 << 22
}
