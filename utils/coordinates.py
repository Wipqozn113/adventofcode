import math

class Line:
    def __init__(self, coord1, coord2):        
        self.start = coord1 if coord1.x < coord2.x else coord2
        self.end = coord1 if coord1.x >= coord2.x else coord2

    def JoinLines(self, line):
        


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
