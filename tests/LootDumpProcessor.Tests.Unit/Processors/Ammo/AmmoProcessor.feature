Feature: Ammo Distribution

    Scenario: Create ammo distribution with valid containers
        Given the following ammo:
          | Tpl   | Caliber |
          | ammo1 | 5.56x45 |
          | ammo2 | 5.56x45 |
        And a container with items:
          | Tpl   |
          | ammo1 |
          | ammo2 |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |
          | 5.56x45 |
        And the calibers counts should be:
          | Caliber | Count |
          | 5.56x45 | 2     |
        And relative probabilities should be:
          | Caliber | Relative probability |
          | 5.56x45 | 2                    |

    Scenario: Create ammo distribution with should return empty distribution
        Given the following ammo:
          | Tpl | Caliber |
        And a container with items:
          | Tpl |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |

    Scenario: Create ammo distribution with no matching containers
        Given the following ammo:
          | Tpl   | Caliber |
          | ammo3 | 7.62x39 |
        And a container with items:
          | Tpl   |
          | ammo1 |
          | ammo2 |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |

    Scenario: Create ammo distribution with multiple calibers
        Given the following ammo:
          | Tpl   | Caliber |
          | ammo1 | 5.56x45 |
          | ammo2 | 7.62x39 |
        And a container with items:
          | Tpl   |
          | ammo1 |
          | ammo2 |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |
          | 5.56x45 |
          | 7.62x39 |
        And the calibers counts should be:
          | Caliber | Count |
          | 5.56x45 | 1     |
          | 7.62x39 | 1     |
        And relative probabilities should be:
          | Caliber | Relative probability |
          | 5.56x45 | 1                    |
          | 7.62x39 | 1                    |

    Scenario: Create ammo distribution with duplicate container ammo entries
        Given the following ammo:
          | Tpl   | Caliber |
          | ammo1 | 5.56x45 |
        And a container with items:
          | Tpl   |
          | ammo1 |
          | ammo1 |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |
          | 5.56x45 |
        And the calibers counts should be:
          | Caliber | Count |
          | 5.56x45 | 1     |
        And relative probabilities should be:
          | Caliber | Relative probability |
          | 5.56x45 | 1                    |

    Scenario: Create ammo distribution with multiple containers
        Given the following ammo:
          | Tpl   | Caliber |
          | ammo1 | 5.56x45 |
          | ammo2 | 5.45x39 |
        And a container with items:
          | Tpl   |
          | ammo1 |
          | ammo2 |
        And a container with items:
          | Tpl   |
          | ammo1 |
        And a container with items:
          | Tpl   |
          | ammo1 |
        When I create an ammo distribution
        Then the distribution should only contain the following calibers:
          | Caliber |
          | 5.56x45 |
          | 5.45x39 |
        And the calibers counts should be:
          | Caliber | Count |
          | 5.56x45 | 1     |
          | 5.45x39 | 1     |
        And relative probabilities should be:
          | Caliber | Relative probability |
          | 5.56x45 | 1                    |
          | 5.45x39 | 1                    |