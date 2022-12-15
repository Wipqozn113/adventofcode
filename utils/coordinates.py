import math

class Coordinate:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y)

    def __hash__(self):
        return hash((self.x, self.y))

    # https://en.wikipedia.org/wiki/Taxicab_geometry
    def ManhattenDistance(self, other):
        if not isinstance(other, Coordinate):
            raise Exception("Object must be a Coordinate")
        
        return math.abs(self.x - other.x) + math.abs(self.y - other.y)
