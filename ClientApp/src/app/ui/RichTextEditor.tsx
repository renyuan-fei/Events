import { forwardRef, useImperativeHandle } from 'react';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css'; // 导入样式

interface RichTextEditorProps {
    onChange: (content: string, delta: any, source: string, editor: any) => void;
    value: string;
}

// 使用 forwardRef 包裹组件
const RichTextEditor = forwardRef((props: RichTextEditorProps, ref) => {
    const { onChange, value } = props;

    // 使用 useImperativeHandle 自定义暴露给父组件的实例值
    useImperativeHandle(ref, () => ({
        // 你可以在这里添加更多的方法或属性，这些可以被父组件调用
    }));

    return (
        <ReactQuill
            value={value}
            onChange={onChange}
            style={{ height: '85%', width: '100%' }}  // 设置编辑器的宽度和高度
        />
    );
});

export default RichTextEditor;
