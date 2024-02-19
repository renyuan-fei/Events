import { useEffect, useState } from 'react';
import { FieldErrors } from 'react-hook-form';

const useDynamicFormHeight = (errors: FieldErrors, baseHeight: number, errorHeightIncrement: number) => {
    const [height, setHeight] = useState(baseHeight);

    useEffect(() => {
        // 计算具有实际错误信息的字段数量
        const errorCount = Object.values(errors).filter(error => error).length;
        // 根据错误数量调整对话框高度
        setHeight(baseHeight + errorHeightIncrement * errorCount);
    }, [errors, baseHeight, errorHeightIncrement]);

    return height;
};

export default useDynamicFormHeight;
