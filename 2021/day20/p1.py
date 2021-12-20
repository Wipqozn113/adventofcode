from copy import deepcopy

class Algorithm:
    def __init__(self, algo):
        self.iea = algo
    
    def Run(self, inp):
        num = int(inp, 2)

        return self.iea[num]
    
    @property
    def IsBlinking(self):
        return self.iea[0] == "#"

class Image:
    def __init__(self, algo):
        self.image = []
        self.iea = algo

    def AddRow(self, row):
        self.image.append(list(row))

    @property
    def Width(self):
        return len(self.image[0])

    @property
    def Height(self):
        return len(self.image)

    @property 
    def LitPixels(self):
        count = 0
        for y in range(self.Height):
            for x in range(self.Width):
                if self.image[y][x] == "#":
                    count += 1

        return count

    def BlinkingPixels(self, count):
        if self.iea.IsBlinking:
            infpixel = "." if count % 2 == 0 else "#"
            infval = "0" if count % 2 == 0 else "1"
        else:
            infpixel = "." 
            infval = "0" 
        return infpixel, infval

    def Enchance(self, count, i):
        infpixel, infval = self.BlinkingPixels(i)
        self.Expand(infpixel)
        output = deepcopy(self.image)

        for y in range(self.Height):
            for x in range(self.Width):
                surr = [[""] * 3, [""] * 3, [""] * 3]
                if y == 0:
                    surr[0][0] = infval
                    surr[0][1] = infval
                    surr[0][2] = infval
                elif y == self.Height - 1:
                    surr[2][0] = infval
                    surr[2][1] = infval
                    surr[2][2] = infval
                
                if x == 0:
                    surr[0][0] = infval
                    surr[1][0] = infval
                    surr[2][0] = infval
                elif x == self.Width - 1:
                    surr[0][2] = infval
                    surr[1][2] = infval
                    surr[2][2] = infval

                for n in range(3):
                    for m in range(3):
                        if surr[n][m] == "":
                            yn = y + n - 1
                            xm = x + m - 1
                            surr[n][m] = "0" if self.image[yn][xm] == "." else "1"

                num = ''.join(surr[0] + surr[1] + surr[2])               
                output[y][x] = self.iea.Run(num)

      
        self.image = deepcopy(output)

        count -= 1
        i += 1

        if count > 0:
            self.Enchance(count, i)
    
    def Print(self):
        print("")
        for row in self.image:
            print(''.join(row))
        print("")

    def Expand(self, pixel):

        for y in range(self.Height):
            self.image[y].insert(0, pixel)
            self.image[y].append(pixel)

        self.image.insert(0, [pixel] * self.Width)
        self.image.append([pixel] * self.Width)

first = True
with open('p1.in') as file:
    for line in file:
        if first:
            algo = Algorithm(line.strip())
            first = False
            image = Image(algo)
            continue
        if line.strip() == "":
            continue

        image.AddRow(line.strip())

image.Enchance(50, 0)
# 5326
print(image.LitPixels)


