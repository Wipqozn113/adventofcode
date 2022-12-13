class Pair:
    def __init__(self, packet1, packet2, index):
        self.packet1 = packet1
        self.packet2 = packet2
        self.index = index

    def ComparePairs(self):
        corr_order = self.__compare(self.packet1, self.packet2)

        if corr_order:
            return self.index
        else:
            return 0

    def __compare(self, packet1, packet2):
        # handle convert
        if isinstance(packet1, int):
            p1 = [packet1]
        else:
            p1 = packet1

        if isinstance(packet2, int):
            p2 = [packet2]
        else:
            p2 = packet2

        # Cache p2 length
        p2len = len(p2)
        for i in range(len(p1)):
            # p1 is longer than p2, so wrong order
            if i == p2len:
                return False

            # One value is a list, so we need to recurse
            if isinstance(p1[i], list) or isinstance(p2[i], list):
                corr_order = self.__compare(p1[i], p2[i])
                if corr_order is None:
                    continue
                else:
                    return corr_order
            # Both values are integers, so we can actually compare
            else:
                # Correct Order
                if p1[i] < p2[i]:
                    return True
                # Wrong Order
                elif p1[i] > p2[i]:
                    return False
        
        if(len(p1) == p2len):
            return None
        else:
            return len(p1) < p2len




def CreatePairs(filename):
    pairs = []
    p1 = None
    p2 = None
    index = 1
    with open(filename) as file:
        for line in file:
            if p1 is None:
                p1 = eval(line.strip())
            elif p2 is None:
                p2 = eval(line.strip())
                pairs.append(Pair(p1, p2, index))
                index += 1
            else:
                p1 = None
                p2 = None
    
    return pairs
            
pairs = CreatePairs("input.in")
psum = 0
for pair in pairs:
    psum += pair.ComparePairs()

print(psum)