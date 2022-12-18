import math

class Coordinate3D:
    def __init__(self, x, y, z):
        self.x = int(x)
        self.y = int(y)
        self.z = int(z)

    @property
    def Key(self):
        return self.__hash__()

    @property
    def LargestCoord(self):
        return max(self.x, self.y, self.z)

    def AdjCoordinates(self):
        adjcoords = []
        x,y,z = self.x,self.y,self.z

        adjcoords.append(Coordinate3D(x+1,y,z))
        adjcoords.append(Coordinate3D(x-1,y,z))

        adjcoords.append(Coordinate3D(x,y+1,z))
        adjcoords.append(Coordinate3D(x,y-1,z))

        adjcoords.append(Coordinate3D(x,y,z+1))
        adjcoords.append(Coordinate3D(x,y,z-1))        

        return adjcoords

    def AreAdj(self, other):
        sub = self - other
        return abs(sub.x) + abs(sub.y) + abs(sub.z) == 1

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y) and (self.z == other.z)

    # @ dot product?
    def __matmul__(self, other):
        return (self.x * other.x) + (self.y * other.y)

    def __sub__(self, other):
        return Coordinate3D(self.x - other.x, self.y - other.y, self.z - other.z)

    def __hash__(self):
        return hash((self.x, self.y, self.z))

    # https://en.wikipedia.org/wiki/Taxicab_geometry
    def ManhattenDistance(self, other):
        if not isinstance(other, Coordinate3D):
            raise Exception("Object must be a Coordinate3D")
        
        return abs(self.x - other.x) + abs(self.y - other.y) + abs(self.z - other.z)

