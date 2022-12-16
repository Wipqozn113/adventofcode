import math

class HorizontalLine:
    def __init__(self, coord1, coord2): 
        if(coord1.y != coord2.y):
            raise Exception("Horizatonal lines only!")

        c1 = Coordinate(coord1.x, coord1.y)
        c2 = Coordinate(coord2.x, coord2.y)       
        self.start = c1 if c1.x <= c2.x else c2
        self.end = c1 if c1.x > c2.x else c2
        
    # Trim a line to not exceed a boundry
    def Trim(self, startx, endx):
        if self.start.x < startx:
            self.start.x = startx
        if self.end.x > endx:
            self.end.x = endx

    def PrintMe(self):
        print("Start: ", self.start.x, "End: ", self.end.x, "Y: ", self.start.y)

    def __hash__(self):
        return hash((self.start.__hash__(), self.end.__hash__()))

    def CanJoin(self, other):
        if self.start.y != other.start.y:
            return False
        return self.LinesIntersect(other) or self.LinesAdj(other)

    def LinesIntersect(self, other):
        # If LinedsEmbed, they also intersect
        if self.EmbeddedLines(other):
            return True

        # Make ln1 the line further to the left
        if self.start.x >= other.start.x:
            ln1 = other
            ln2 = self
        else:
            ln1 = self
            ln2 = other

        # Ln1 ends before ln2 starts
        if ln1.end.x < ln2.start.x:
            return False
        
        # Ln1 ends witihn Ln2
        if ln2.end.x >= ln1.end.x >= ln2.start.x:
            return True
        
        return False        

    def LinesAdj(self, other):
        # Not Adjanact if intersect
        if self.LinesIntersect(other):
            return False

        if abs(self.end.x - other.start.x) == 1:
            return True
        if abs(other.end.x - self.start.x) == 1:
            return True

        return False

    def EmbeddedLines(self, other):
        return self.LineContains(other) or self.LineContainedBy(other)

    def LineContains(self, other):
        return (self.start.x <= other.start.x) and (self.end.x >= other.end.x)

    def LineContainedBy(self, other):
        return other.LineContains(self)
        
    def JoinLines(self, other):
        if self.CanJoin(other):
            # Extend Start and end of line as requried
            if self.start.x > other.start.x:
                self.start.x = other.start.x
            if self.end.x < other.end.x:
                self.end.x = other.end.x

            # Delete Old Line
            del(other)

class Rhombus:
    def __init__(self, centre, radius_x, radius_y=None):
        self.centre = Coordinate(centre.x, centre.y)
        self.rx = radius_x
        
        # If only radius_x is provided, assume radius of x and y are identical
        self.ry = radius_x if radius_y is None else radius_y
        '''
        self.area = (self.rx * self.ry) / 2
        self.xl = Coordinate(self.centre.x - self.rx)
        self.xr = Coordinate(self.centre.x + self.rx)
        self.yt = Coordinate(self.centre.y + self.ry)
        self.yb = Coordinate(self.centre.y - self.ry)
        '''

    def LineFromCentreY(self, distance_y):
        # Too far away
        if abs(distance_y) > self.ry:
            return None
        y = self.centre.y + distance_y

        # Row is close enough to be contained within zone
        col_length = 2 * (self.ry - abs(distance_y)) + 1
        col_radius = math.floor(col_length / 2)        
        
        # Create line
        start = Coordinate(self.centre.x - col_radius, y)
        end = Coordinate(self.centre.x + col_radius, y)
        line = HorizontalLine(start, end)

        return line

    # NOT DONE: https://math.stackexchange.com/questions/312403/how-do-i-determine-if-a-point-is-within-a-rhombus
    def ContainsCoordinate(self, coordinate):
        w = coordinate - self.centre
        xabs = abs(self.vx @ w)
        yabs = abs(self.vy @ w)

        return (((xabs/self.rx) + (yabs/self.ry)) <=1)

class Coordinate:
    def __init__(self, x, y):
        self.x = int(x)
        self.y = int(y)

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y)

    # @ dot product?
    def __matmul__(self, other):
        return (self.x * other.x) + (self.y * other.y)

    def __sub__(self, other):
        return Coordinate(self.x - other.x, self.y - other.y)

    def __hash__(self):
        return hash((self.x, self.y))

    # https://en.wikipedia.org/wiki/Taxicab_geometry
    def ManhattenDistance(self, other):
        if not isinstance(other, Coordinate):
            raise Exception("Object must be a Coordinate")
        
        return abs(self.x - other.x) + abs(self.y - other.y)
