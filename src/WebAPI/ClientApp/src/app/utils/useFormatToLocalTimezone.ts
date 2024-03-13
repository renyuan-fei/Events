const useFormatToLocalTimezone = (dateInput: Date | string): string => {
    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;

    // 尝试解析输入的日期时间字符串或对象
    let date: Date;
    try {
        if (typeof dateInput === 'string') {
            // 检查输入字符串是否已经包含时区信息
            date = new Date(dateInput.match(/Z$/) ? dateInput : dateInput + 'Z');
        } else {
            date = new Date(dateInput.toISOString());
        }

        // 确保解析得到的日期是有效的
        if (isNaN(date.getTime())) {
            throw new Error('Invalid date');
        }
    } catch (error) {
        console.error('Error parsing date:', error);
        return 'Invalid date'; // 或者根据你的需求返回一个默认值或错误信息
    }

    const options: Intl.DateTimeFormatOptions = {
        weekday: 'short',
        month: 'short',
        day: 'numeric',
        hour: 'numeric',
        minute: '2-digit',
        timeZoneName: 'short',
        hour12: true,
        timeZone,
    };

    return new Intl.DateTimeFormat('en-AU', options).format(date);
};

export default useFormatToLocalTimezone;
