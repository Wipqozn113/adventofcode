import math

class HorizontalLine:
    def __init__(self, coord1, coord2): 
        if(coord1.y != coord2.y):
            raise Exception("Horizatonal lines only!")

        c1 = Coordinate(coord1.x, coord1.y)
        c2 = Coordinate(coord2.x, coord2.y)       
        self.start = c1 if c1.x <= c2.x else c2
        self.end = c1 if c1.x > c2.x else c2
    
    def __hash__(self):
        return hash((self.start.__hash__(), self.end.__hash__()))

    def CanJoin(self, other):
        if self.start.y != other.start.y:
            return False

        # Liens Intersect
        if other.start.x <= self.start.x <= other.end.x:
            return True
        if other.start.x <= self.end.x <= other.end.x:
            return True

        return False

    def JoinLines(self, other):
        if self.CanJoin(other):
            # Extend Start and end of line as requried
            if self.start.x > other.start.x:
                self.start.x = other.start.x
            if self.end.x < other.end.x:
                self.end.x = other.end.x

            # Delete Old Line
            del(other)


class Coordinate:
    def __init__(self, x, y):
        self.x = int(x)
        self.y = int(y)

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y)

    def __hash__(self):
        return hash((self.x, self.y))

    # https://en.wikipedia.org/wiki/Taxicab_geometry
    def ManhattenDistance(self, other):
        if not isinstance(other, Coordinate):
            raise Exception("Object must be a Coordinate")
        
        return abs(self.x - other.x) + abs(self.y - other.y)
