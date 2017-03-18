#ifndef __MESSAGE_H__
#define __MESSAGE_H__

#include "UnManagedMessage.h"
namespace ons{

    //<!***************************************************************************
    public ref class SystemPropertyKey
    {
        public:
           SystemPropertyKey(){}
           ~SystemPropertyKey(){}
           static System::String^ TAG = "__TAG";
	    static System::String^ KEY = "__KEY";
	    static System::String^ MSGID = "__MSGID";
	    static System::String^ RECONSUMETIMES = "__RECONSUMETIMES";
	    static System::String^ STARTDELIVERTIME = "__STARTDELIVERTIME";
    };
    
    public ref class  Message
    {        
    public:
            Message();
            Message(Message% other);
            Message(UnmanagedMessage msg);
            Message(System::String^ topic, System::String^ tags, System::String^ body);
            Message(System::String^ topic, System::String^ tags,   System::String^ keys,   System::String^ body);
            virtual ~Message();
            !Message();

            UnmanagedMessage *getMessage();
            
            System::String^  getTopic();
            void    setTopic(System::String^ topic);
    
            System::String^  getTag();
            void    setTag(System::String^ tags);
    
            System::String^  getKey();
            void    setKey(System::String^ keys);

            System::String^ getMsgID();
            void   setMsgID(System::String^ msgId);
    
            __int64   getStartDeliverTime();
            void   setStartDeliverTime(__int64 level);
    
            System::String^ getBody();
            void   setBody(System::String^ msgbody);
    
            int     getReconsumeTimes();
            void    setReconsumeTimes(int reconsumeTimes);

            //userProperties was used to save user specific parameters which doesn't belong to SystemPropKey
            void putUserProperty(System::String^ userPropertyKey, System::String^ userPropertyValue);//set user property
            System::String^ getUserProperty(System::String^ userPropertyKey);
            
            //systemProperties only save parameters defined in SystemPropertyKey, please do not add other parameters into systemProperties
            void   putSystemProperty(System::String^ key, System::String^ value);
            System::String^ getSystemProperty(System::String^ userPropertyKey);

    private:
            UnmanagedMessage *m_message;
    };
    //<!***************************************************************************
    

}
#endif
