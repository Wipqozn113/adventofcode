import string

class Backpack:
    def __init__(self, items):
        self.items = items
        self.comp1 = self.Comp1
        self.comp2 = self.Comp2
        self.priorities = list(string.ascii_lowercase) + list(string.ascii_uppercase)

    @property
    def Size(self):
        return len(self.items)

    @property
    def Half(self):
        return int(self.Size/2)

    @property
    def Comp1(self):
        return self.items[:self.Half]

    @property
    def Comp2(self):
        return self.items[self.Half:]

    def ItemPriority(self, item):
        return self.priorities.index(item) + 1

    @property
    def Priority(self):
        return self.priorities.index(self.FindDup()) + 1

    def FindDup(self):
        item = list(set(self.comp1) & set(self.comp2))[0]
        return item


class Group:
    def __init__(self, packs):
        self.p1 = packs[0]
        self.p2 = packs[1]
        self.p3 = packs[2]

    @property
    def BadgePriority(self):
        badge = list(set(self.p1.items) & set(self.p2.items) & set(self.p3.items))
        return self.p1.ItemPriority(badge[0])

packs = []
grsum = 0
with open('input.in') as file:
    for line in file:      
        packs.append(Backpack(line.strip()))
        if(len(packs) == 3):
            group = Group(packs)
            packs.clear()
            grsum += group.BadgePriority

print(grsum)
