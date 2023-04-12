class Elf
    @calories

    def initialize()
        @calories = 0
    end

    def cals 
        @calories
    end

    def addCalories(cal)          
        @calories = @calories + Integer(cal)
    end
end

bestElf = Elf.new
currentElf = Elf.new
File.foreach("input.in") do |line|
    if line.strip.empty?
        if currentElf.cals > bestElf.cals
            bestElf = currentElf            
        end
        currentElf = Elf.new
    else
        currentElf.addCalories(line)
    end
end
puts bestElf.cals