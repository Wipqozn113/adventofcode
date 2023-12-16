class Step:
    def __init__(self, step):
        self.step = step
        self.hash_value = None

    def calc_hash(self):
        if self.hash_value is not None:
            return self.hash_value
        
        '''
        Determine the ASCII code for the current character of the string.
        Increase the current value by the ASCII code you just determined.
        Set the current value to itself multiplied by 17.
        Set the current value to the remainder of dividing itself by 256.
        '''
        current_value = 0
        for char in self.step:
            current_value += ord(char)
            current_value *= 17
            current_value = current_value % 256

        self.hash_value = current_value

        return self.hash_value
    
steps = []
filename = "input.in"
with open(filename) as  file:
    for line in file:
        line = line.split(",")
        for step in line:
            steps.append(Step(step))

total = 0
for step in steps:
    total += step.calc_hash()
print(total)

