class Area:
    def __init__(self, area):
        a = area.split("-")
        self.x = a[0]
        self.y = a[1]
        self.s = set(range(int(self.x), int(self.y)+1))

    def ContainedBy(self, other):
       return self.s <= other.s

class Pair:
    def __init__(self, pair):
        p = pair.strip().split(",")
        self.a1 = Area(p[0])
        self.a2 = Area(p[1])

    def FullyContainedPair(self):
        a1, a2 = self.a1,self.a2 # After spending so much time with C#, I really fucking hate self.Member
        return a1.ContainedBy(a2) or a2.ContainedBy(a1)

c = 0
with open('input.in') as file:
    for line in file:
        pair = Pair(line)
        if(pair.FullyContainedPair()):
            c += 1

print(c)

        