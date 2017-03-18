#ifndef __SENDRESULTONS_H__
#define __SENDRESULTONS_H__


namespace ons{

public ref class SendResultONS{
    public:
        SendResultONS();
        virtual ~SendResultONS();

        void setMessageId(System::String^ msgId);
        System::String^ getMessageId();

    private:
        System::String^ m_msgId;
};

}

#endif
