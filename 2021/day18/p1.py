import math

class SnailfishNumber:
    def __init__(self, pairs, parent=None):
        self.left = pairs[0] if isinstance(pairs[0], int) or isinstance(pairs[0], SnailfishNumber) else SnailfishNumber(pairs[0], self)
        self.right = pairs[1] if isinstance(pairs[1], int) or isinstance(pairs[1], SnailfishNumber) else SnailfishNumber(pairs[1], self)
        self.parent = parent

    def Reduce(self):     
        i = 0
        while True:
            exploded = self.Explode(0)
            if exploded:
              #  print(self.Print())
                continue
            split = self.Split()
            if split:
              #  print(self.Print())
                continue

            break

    def Print(self):
        if isinstance(self.left, int) and isinstance(self.right, int):
            return f"[{self.left}, {self.right}]"

        x = self.left if isinstance(self.left, int) else self.left.Print()
        y = self.right if isinstance(self.right, int) else self.right.Print()

        return f"[{x}, {y}]"

    def Explode(self, nested):
        # Can't crawl any deeper, and not at exploded threshold
        if isinstance(self.left, int) and isinstance(self.right, int) and nested < 4:
            return False

        if isinstance(self.left, SnailfishNumber):
            if self.left.Explode(nested + 1):
                return True
            
        if isinstance(self.right, SnailfishNumber):
            if self.right.Explode(nested + 1):
                return True

        # At nested limit. Explode.
        # NEED TO HANDLE ZEROING
        if nested == 4:
            if self.parent.left == self:
                par = self.parent
                child = self
                while True:
                    if par is None:
                        break
                    if par.left == child:
                        child = par
                        par = par.parent
                    else:
                        if isinstance(par.left, int):
                            par.left += self.left
                            break

                        par = par.left
                        while True:  
                            if isinstance(par.right, int):
                                par.right += self.left
                                break
                            par = par.right
                        break
                if isinstance(self.parent.right, SnailfishNumber):
                    self.parent.right.left += self.right
                else:
                    self.parent.right += self.right
                self.parent.left = 0

            elif self.parent.right == self:
                self.parent.left += self.left
                par = self.parent
                child = self
                while True:
                    if par is None:
                        break
                    if par.right == child:
                        child = par
                        par = par.parent
                    else:
                        if isinstance(par.right, int):
                            par.right += self.right
                            break

                        par = par.right
                        while True:  
                            if isinstance(par.left, int):
                                par.left += self.right
                                break
                            par = par.left
                        break
                self.parent.right = 0

            return True               

        return False

    def Split(self):
        if isinstance(self.left, int) and self.left > 9:
            l = math.floor(self.left/2)
            r = math.ceil(self.left/2)
            self.left = SnailfishNumber([l, r], self)

            return True
        
        if isinstance(self.right, int) and self.right > 9:
            l = math.floor(self.right/2)
            r = math.ceil(self.right/2)
            self.right = SnailfishNumber([l, r], self)

            return True

        if isinstance(self.left, SnailfishNumber):
            if self.left.Split():
                return True

        if isinstance(self.right, SnailfishNumber):
            if self.right.Split():
                return True

        return False

    @property
    def Magnitude(self):
        left = 0 
        right = 0
        if(isinstance(self.left, int)):
            left = 3 * self.left
        else:
            left = 3 * self.left.Magnitude
        
        if(isinstance(self.right, int)):
            right = 2 * self.right
        else:
            right = 2 * self.right.Magnitude

        return left + right

    # [1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]]
    def __add__(self, other):
        number = SnailfishNumber([self, other])        
        self = number
        self.left.parent = self
        self.right.parent = self  
        print(self.Print())

        self.Reduce() 

        return self      


numbers = []
number = None
with open('s4.in') as file:
    for line in file:
        exec("number = " + line.strip())
        numbers.append(SnailfishNumber(number))

num = numbers.pop(0)
while len(numbers) > 0:  
    num += numbers.pop(0)
    print(num.Print())


print(num.Magnitude)        


