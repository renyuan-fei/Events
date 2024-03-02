const useFormatToLocalTimezone = (dateInput: Date | string): string => {
    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    // 检查dateInput类型并相应处理
    const date = new Date(typeof dateInput === 'string' ? dateInput + 'Z' : dateInput.toISOString() + 'Z');

    const options: Intl.DateTimeFormatOptions = {
        weekday: 'short', // e.g., "Fri"
        month: 'short',   // e.g., "Jan"
        day: 'numeric',   // e.g., "19"
        hour: 'numeric',  // e.g., "7"
        minute: '2-digit', // e.g., "30"
        timeZoneName: 'short', // e.g., "ACDT"
        hour12: true,     // 12-hour time
        timeZone: timeZone, // Set the desired timezone
    };

    return new Intl.DateTimeFormat('en-AU', options).format(date);
};

export default useFormatToLocalTimezone;
