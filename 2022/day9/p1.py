class Knot:
    def __init__(self):
        self.x = 0
        self.y = 0
        self.visited = set(['0,0'])

    def VisitedCount(self):
        return len(self.visited)

    def AreAdj(self, other):
        if (self.x == other.x) and (self.y == other.y):
            return True
        
        if self.x == other.x and abs(self.y - other.y) == 1:
            return True
        
        if self.y == other.y and abs(self.x - other.x) == 1:
            return True

        if abs(self.y - other.y) == 1 and abs(self.x - other.x) == 1:
            return True

        return False
    
    def HeadMove(self, direction, distance):
        if direction == "D":
            self.y -= distance
        elif direction == "U":
            self.y += distance
        elif direction == "R":
            self.x += distance
        elif direction == "L":
            self.x -= distance

        self.visited.add('{},{}'.format(self.x, self.y))
    
    def TailMove(self, head):
        # If Adj or touching, nothing to do
        if self.AreAdj(head):
            return

        while not self.AreAdj(head):
            if self.x == head.x:
                self.y += 1 if head.y > self.y else -1
            elif self.y == head.y:
                self.x += 1 if head.x > self.x else -1
            else:
                self.y += 1 if head.y > self.y else -1
                self.x += 1 if head.x > self.x else -1

            self.visited.add('{},{}'.format(self.x, self.y))
        return
        
head = Knot()
tail = Knot()
with open('input.in') as file:
    for line in file:
        l = line.strip().split(' ')
        head.HeadMove(l[0].strip(), int(l[1].strip()))
        tail.TailMove(head)

print(tail.VisitedCount())