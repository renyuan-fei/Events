const useFormatToLocalTimezone = (dateString: string): string => {
    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const date = new Date(dateString + 'Z'); // Parse the date string as UTC

    // Set up the options for formatting
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

    // Format the date in the given timezone
    return new Intl.DateTimeFormat('en-AU', options).format(date);
}

export default useFormatToLocalTimezone;