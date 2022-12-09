class Knot:
    def __init__(self, remaining_knots):
        self.x = 0
        self.y = 0
        self.visited = set(['0,0'])
        self.child = None
        if remaining_knots > 0:
            self.child = Knot(remaining_knots - 1)

    def TailVisitedCount(self):
        print("what")
        if self.child is None:
            return self.VisitedCount()
        else:
            return self.child.TailVisitedCount()

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
    
    def Move(self, direction, distance):
        while distance > 0:
            distance -= 1
            if direction == "D":
                self.y -= 1
            elif direction == "U":
                self.y += 1
            elif direction == "R":
                self.x += 1
            elif direction == "L":
                self.x -= 1

            self.visited.add('{},{}'.format(self.x, self.y))
            if self.child is not None:
                self.child.ChildMove(self)
    
    def ChildMove(self, head):
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
            if self.child is not None:
                self.child.ChildMove(self)
        

        
head = Knot(9)
with open('input.in') as file:
    for line in file:
        l = line.strip().split(' ')
        head.Move(l[0].strip(), int(l[1].strip()))

print(head.TailVisitedCount())