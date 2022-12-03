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

pri = 0
with open('input.in') as file:
    for line in file:
        bp = Backpack(line.strip())
        pri += bp.Priority

print(pri)
