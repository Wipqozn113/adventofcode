"""
{ 0:6, 1:2, 2:5, 3:5, 4:4, 5:5, 6:6, 7:3, 8:7, 9:6 }
{ 1:2 }
{ 7:3 }
{ 4:4} 
{ 2:5, 3:5, 5:5 }
{ 0:6, 6:6,  9:6 }
{ 8:7 }
"""

class Number:
    def __init__(self, val, signal):
        self.val = val
        self.signal = signal

    def __eq__(self, other):
        """Overrides the default implementation"""
        if isinstance(other, Number):
            return self.val == other.val
        elif isinstance(other, str):
            diff = set(self.signal).symmetric_difference(set(other))
            return True if len(diff) == 0 else False
        return NotImplemented

class Decoder:
    def Decode(self, input):
        patterns, output = input.strip().split("|")
        numbers = self.DecodePatterns(patterns.strip().split(" "))
        return self.DecodeOutput(numbers, output.strip())

    def DecodeOutput(self, numbers, output):
        out = output.split(" ")
        val = ""
        for o in out:
            for num in numbers:
                if numbers[num] == o:
                    val += str(numbers[num].val)
                    break

        return int(val)

    def DecodePatterns(self, patterns):
        numbers = {}
        numbers[1] = Number(1, self.Find1(patterns)) # 0
        numbers[4] = Number(4, self.Find4(patterns)) # 1
        numbers[7] = Number(7, self.Find7(patterns)) # 2
        numbers[8] = Number(8, self.Find8(patterns)) # 3
        numbers[3] = Number(3, self.Find3(patterns, numbers[1].signal)) # 4
        numbers[9] = Number(9, self.Find9(patterns, numbers[4].signal)) # 5
        numbers[0] = Number(0, self.Find0(patterns, numbers[1].signal, numbers[9].signal)) # 6
        numbers[5] = Number(5, self.Find5(patterns, numbers[9].signal, numbers[3].signal)) # 7
        numbers[2] = Number(2, self.Find2(patterns, numbers[3].signal, numbers[5].signal)) # 8
        numbers[6] = Number(6, self.Find6(patterns, numbers[0].signal, numbers[9].signal)) # 9

        return numbers

    def Find0(self, patterns, one, nine):
        for pattern in patterns:
            if((len(pattern) == 6) and (pattern != nine) and (one[0] in pattern) and (one[1] in pattern)):
                return pattern

    def Find1(self, patterns):
        for pattern in patterns:
            if(len(pattern) == 2):
                return pattern

    def Find2(self, patterns, three, five):
        for pattern in patterns:
            if((len(pattern) == 5) and (pattern != three) and (pattern != five)):
                return pattern

    def Find3(self, patterns, one):        
        for pattern in patterns:
            if((len(pattern) == 5) and (one[0] in pattern) and (one[1] in pattern)):
                return pattern
                
    def Find4(self, patterns):
        for pattern in patterns:
            if(len(pattern) == 4):
                return pattern

    def Find5(self, patterns, nine, three):
        for pattern in patterns:
            if((len(pattern) == 5) and (pattern != three)):
                diff = 0
                for char in nine:
                    if char not in pattern:
                        diff += 1

                if(diff == 1):
                    return pattern

    def Find6(self, patterns, zero, nine):
        for pattern in patterns:
            if((len(pattern) == 6) and (pattern != zero) and (pattern != nine)):
                return pattern

    def Find7(self, patterns):
        for pattern in patterns:
            if(len(pattern) == 3):
                return pattern

    def Find8(self, patterns):
        for pattern in patterns:
            if(len(pattern) == 7):
                return pattern

    def Find9(self, patterns, four):
        for pattern in patterns:
            if((len(pattern) == 6) and (four[0] in pattern) and (four[1] in pattern) and (four[2] in pattern) and (four[3] in pattern)):
                return pattern

decoder = Decoder()
sum = 0
with open('p1.in') as file:
    for line in file:
        sum += decoder.Decode(line)

print(sum)

